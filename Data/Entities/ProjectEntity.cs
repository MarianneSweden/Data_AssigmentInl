using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class ProjectEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //Ska ska auto ID på projektet
        public int Id { get; set; } //Projektnummer.. nåt galet blir det..

        [Required]
        [MaxLength(20)]
        public string ProjectNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string ProjectName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public virtual CustomerEntity? Customer { get; set; }

        [Required]
        [ForeignKey("StatusType")]
        public int StatusId { get; set; }
        public virtual StatusTypeEntity? StatusType { get; set; }

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity? User { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity? Product { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? EstimatedHours { get; set; }

        [Required]

        [Column(TypeName = "decimal(18,2)")]
        public decimal? PricePerHour { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal? TotalPrice { get; set; }

        public void CalculateTotalPrice()
        {
            if (PricePerHour.HasValue && EstimatedHours.HasValue)
            {
                TotalPrice = PricePerHour.Value * EstimatedHours.Value;
            }
            else
            {
                TotalPrice = 0;
            }
        }
    }
}
