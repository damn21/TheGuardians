﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TheGuardians.Models;

namespace TheGuardians.DTOs
{
    public class HeroeCreationDTO
    {
        //[Key]
        //public int HeroeId { get; set; }

        //[ForeignKey("IdPersona")]
        //public int IdPersona { get; set; }

        public virtual PersonaDTO IdPersonaNavigation { get; set; }

    }
}
