using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgeRanger.Controllers;
using AgeRanger.Models;
using Microsoft.Ajax.Utilities;
using System.Linq.Expressions;

namespace AgeRanger
{
    public class AgeGroupViewModel
    {
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        public string LastName { get; set; }

        public int? Age { get; set; }
        public IEnumerable<Person> Persons { get; set; }

        public string SearchTerm { get; set; }
        public string Action
        {
            get
            {
                Expression<Func<HomeController, ActionResult>> update = (c => c.Update(this,Id));
                Expression<Func<HomeController, ActionResult>> create = (c => c.Create(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

    }
}