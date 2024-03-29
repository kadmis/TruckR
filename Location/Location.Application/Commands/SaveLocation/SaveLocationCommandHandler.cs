﻿using BuildingBlocks.Application.Handlers;
using Location.Application.DTO;
using Location.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Location.Application.Commands.SaveLocation
{
    public class SaveLocationCommandHandler : ICommandHandler<SaveLocationCommand>
    {
        private readonly ITemporaryStorageService<LocationModel> _storage;

        public SaveLocationCommandHandler(
            ITemporaryStorageService<LocationModel> storage)
        {
            _storage = storage;
        }

        public async Task<Unit> Handle(SaveLocationCommand request, CancellationToken cancellationToken)
        {
            await _storage.AddAsync(new LocationModel(Guid.Parse(request.UserId), request.Coordinates.Latitude, request.Coordinates.Longitude));

            return Unit.Value;
        }
    }
}
