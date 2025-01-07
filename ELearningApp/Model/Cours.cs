﻿using ELearningApp.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ELearningApp.Model
{
    public class Cours
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Le nom du cours est obligatoire.")]
        [StringLength(100, ErrorMessage = "Le nom du cours ne peut pas dépasser 100 caractères.")]
        [Display(Name = "Nom du cours")]
        public string Nom { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "La description du cours est obligatoire.")]
        [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères.")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Range(0, 5, ErrorMessage = "L'évaluation doit être comprise entre 0 et 5.")]
        [Display(Name = "Évaluation")]
        public float? Evaluation { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Identifiant de la catégorie")]
        public int? CategoryId { get; set; }

        [Display(Name = "Catégorie")]
        public CategoryCours Category { get; set; }

        [ForeignKey("Enseignant")]
        [Display(Name = "Enseignant")]
        public string? EnseignantId { get; set; }

        [Display(Name = "Durée")]
        public double Duree { get; set; }

        [Required(ErrorMessage = "L'image du cours est obligatoire.")]
        [Display(Name = "CoursImg")]
        public string CoursImg { get; set; }

        [Required(ErrorMessage = "L'image du cours est obligatoire.")]
        [Display(Name = "CoursImgPublicId")]
        public string CoursImgPublicId { get; set; }

        [Display(Name = "NombreVideos")]
        public int nbVids { get; set; }

        public Enseignant Enseignant { get; set; }

        public ICollection<Section> sections { get; set; }

        public Examen Examen { get; set; }

        // Nouvelle propriété pour le niveau
        [Required(ErrorMessage = "Le niveau est obligatoire.")]
        [EnumDataType(typeof(Niveau), ErrorMessage = "Valeur du niveau invalide.")]
        [Display(Name = "Niveau")]
        public Niveau Niveau { get; set; }

        public string FormatDuration()
        {
            int minutes = (int)(this.Duree / 60);
            int remainingSeconds = (int)(Duree % 60);
            return $"{minutes:D2} : {remainingSeconds:D2}"; // Format as mm:ss
        }

        public Cours()
        {
            sections = new List<Section>();
        }
    }

    // Déclaration de l'énumération Niveau
    public enum Niveau
    {
        Débutant,
        Intermédiaire,
        Avancé
    }
}
