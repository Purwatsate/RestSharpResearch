﻿namespace ApiShared.Models
{
    public class ProblemDetailsResponse
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public string? Detail { get; set; }
    }

}
