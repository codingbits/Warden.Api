﻿using System;

namespace Warden.Api.Infrastructure.DTO.Wardens
{
    public class WardenCheckResultStorageDto
    {
        public Guid OrganizationId { get; set; }
        public Guid WardenId { get; set; }
        public DateTime CreatedAt { get; set; }
        public WardenCheckResultDto Check { get; set; }
    }
}