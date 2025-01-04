using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

    namespace SAV.Models
    {
        public class ApplicationUser : IdentityUser
        {
            // Nom complet de l'utilisateur
            public string FullName { get; set; }

            // Adresse de l'utilisateur (par exemple, pour envoyer des pièces de rechange)
            public string Address { get; set; }

            // Rôle spécifique de l'utilisateur (Client ou Responsable SAV)
            public string Role { get; set; }

            // Liste des réclamations associées à cet utilisateur (client)
            public ICollection<Reclamation> Reclamations { get; set; }

            // Liste des interventions associées à cet utilisateur (si l'utilisateur est Responsable SAV)
            public ICollection<Intervention> Interventions { get; set; }

            // Liste des pièces de rechange associées à cet utilisateur (client) en cas d'intervention
            public ICollection<PieceRechange> PiecesRechange { get; set; }

            // Historique des interventions réalisées
            public ICollection<InterventionHistory> InterventionHistories { get; set; }

        }

        // Exemple de classe pour stocker l'historique des interventions
        public class InterventionHistory
        {
            public int Id { get; set; }
            public string Description { get; set; }
            public decimal Cost { get; set; }  // Coût de l'intervention
            public DateTime InterventionDate { get; set; }
            public string Status { get; set; } // Statut de l'intervention : "Terminée", "En cours", etc.
        }
    }

