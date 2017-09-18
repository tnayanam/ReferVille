using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReferVille.CustomValidation
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class FileType : ValidationAttribute, IClientValidatable
    {
        private List<string> ValidExtensions { get; set; }

        public FileType(string fileExtensions)
        {
            ValidExtensions = fileExtensions.Split('|').ToList();
        }

        public override bool IsValid(object value)
        {
            HttpPostedFileBase file = value as HttpPostedFileBase;
            if (file != null)
            {
                var fileName = file.FileName;
                var isValidExtension = ValidExtensions.Any(y => fileName.EndsWith(y));
                return isValidExtension;
            }
            return true;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ValidationType = "filetype",
                ErrorMessage = ErrorMessageString
            };
            rule.ValidationParameters.Add("validtypes", string.Join(",", ValidExtensions));
            yield return rule;
        }
    }
}