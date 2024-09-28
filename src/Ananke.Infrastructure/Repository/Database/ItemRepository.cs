﻿using Ananke.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Ananke.Infrastructure.Repository.Database
{
    public class ItemRepository : IItemRepository
    {
        private readonly AnankeContext _context;

        public ItemRepository(AnankeContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Item item, CancellationToken cancellationToken = default)
        {
            Extension? extension = _context.Extensions.FirstOrDefault(ext => ext.Name == item.Extension.Name);
            if (extension != null)
            {
                item.Extension = extension;
            }

            Folder? folder = _context.Folders.FirstOrDefault(folder => folder.Path == item.Folder.Path);
            if (folder != null)
            {
                item.Folder = folder;
            }

            await _context.Items.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddAllAsync(IEnumerable<Item> items, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var extensions = await _context.Extensions.ToDictionaryAsync(e => e.Name, cancellationToken);
            var folders = await _context.Folders.ToDictionaryAsync(f => f.Path, cancellationToken);

            foreach (var item in items)
            {
                cancellationToken.ThrowIfCancellationRequested();

                if (extensions.TryGetValue(item.Extension.Name, out var extension))
                {
                    item.Extension = extension;
                }
                else
                {
                    await _context.Extensions.AddAsync(item.Extension, cancellationToken);
                    extensions.Add(item.Extension.Name, item.Extension);
                }

                if (folders.TryGetValue(item.Folder.Path, out var folder))
                {
                    item.Folder = folder;
                }
                else
                {
                    await _context.Folders.AddAsync(item.Folder, cancellationToken);
                    folders.Add(item.Folder.Path, item.Folder);
                }

                await _context.Items.AddAsync(item, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Item>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<Item>()
                .Include(item => item.Folder)
                .Include(item => item.Extension)
                .ToListAsync(cancellationToken);
        }

        public async Task<Item?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Item>()
                .Include(item => item.Folder)
                .Include(item => item.Extension)
                .FirstAsync(item => item.Id == id, cancellationToken);
        }

        public async Task RemoveByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var item = await GetByIdAsync(id, cancellationToken);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<Item>> GetAllLastAsync(int limit = 10, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Item>()
                .OrderByDescending(item => item.Id)
                .Take(limit)
                .Include(item => item.Folder)
                .Include(item => item.Extension)
                .ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<Item>().CountAsync(cancellationToken);
        }

        public async Task<IEnumerable<Item>> GetByExtensionsAsync(string[] extensions, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            return await _context.Set<Item>()
                .Include(item => item.Folder)
                .Include(item => item.Extension)
                .Where(i => extensions.Contains(i.Extension.Name))
                .Take(size)
                .Skip(size * (page - 1))
                .ToListAsync(cancellationToken);
        }
    }
}