using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyMainDiplomProject.Areas.Identity.Data;

// Add profile data for application users by adding properties to the MyMainDiplomProjectUser class
public class MyMainDiplomProjectUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName ="nvarchar(100)")]
    public string userNikName { get; set; }

    [PersonalData]
    [Column(TypeName = "datetime")]
    public DateTime dateOfBirthday { get; set; }

    [PersonalData]
    [Column(TypeName = "datetime")]
    public DateTime registerDate { get; set; }
}

