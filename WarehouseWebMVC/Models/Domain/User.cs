﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WarehouseWebMVC.Models.Domain;

public class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long UserId { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [StringLength(255)]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Address { get; set; } = string.Empty;
    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;
    [Required]
    [StringLength(255)]
    public string Password { get; set; } = string.Empty;
    [Required]
    [StringLength(10)]
    public string Role { get; set; } = string.Empty;
    public string Avatar { get; set; } = "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";
    public bool IsLocked {  get; set; } = false;
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }
    public ICollection<Receipt> Receipts { get; } = new List<Receipt>();
    public ICollection<ExpenseReport> SentExpenseReports { get; set; } = new List<ExpenseReport>();
    public ICollection<ExpenseReport> ReceivedExpenseReports { get; set; } = new List<ExpenseReport>();
}
