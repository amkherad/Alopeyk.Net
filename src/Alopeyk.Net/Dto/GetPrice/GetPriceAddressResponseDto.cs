﻿using Alopeyk.Net.Dto;

namespace Alopeyk.Net.Dto.GetPrice
{
    public class GetPriceAddressResponseDto : BaseAddressDto
    {
        public string City { get; set; }

        public string Priority { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
    }
}