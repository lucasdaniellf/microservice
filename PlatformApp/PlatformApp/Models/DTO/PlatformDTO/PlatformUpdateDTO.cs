﻿namespace PlatformApp.Models.DTO.PlatformDTO
{
    public class PlatformUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string Company { get; set; } = null!;
        public int? PlatformTypeId { get; set; }
    }
}
