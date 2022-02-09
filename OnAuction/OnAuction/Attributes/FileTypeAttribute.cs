using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnAuction.Attributes
{
    public class FileTypeAttribute : ValidationAttribute
    {
        private readonly List<string> _types;

        public FileTypeAttribute(string types)
        {
            _types = types.Split(',').ToList();
        }

        public override bool IsValid(object value)
        {
            var file = value as HttpPostedFileBase;

            if (file == null)
            {
                return true;
            }

            foreach(var type in _types)
            {
                if (file.FileName.EndsWith(type))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
