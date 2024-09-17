using Ananke.Application.DTO;
using MediatR;
using System.Numerics;

namespace Ananke.Application.Features.Dashboard.Queries
{
    public record GetDisksUsageQuery() : IRequest<IEnumerable<DiskDTO>> { }

    public class GetDisksUsageQueryHandler : IRequestHandler<GetDisksUsageQuery, IEnumerable<DiskDTO>>
    {
        public Task<IEnumerable<DiskDTO>> Handle(GetDisksUsageQuery request, CancellationToken cancellationToken = default)
        {
            IEnumerable<DiskDTO> result = [];

            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                cancellationToken.ThrowIfCancellationRequested();
                result = result.Append(new()
                {
                    Name = drive.Name,
                    Label = drive.VolumeLabel,
                    TotalSpace = drive.TotalSize,
                    AvailableSpace = drive.AvailableFreeSpace
                });
            }
            return Task.FromResult(result);
        }
    }
}