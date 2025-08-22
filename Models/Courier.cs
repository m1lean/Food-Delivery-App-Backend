﻿namespace FoodDeliveryApp.Models;

public class Courier
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public bool IsAvailable { get; set; } = true;
}