using System;
using System.Collections.Generic;
using System.Text;

namespace SmileDirectClub.Shared.Exceptions
{
    public class BadRequest
    {
		public bool Success => false;
		public string Message => "Bad Request";
		public ICollection<BadRequestField> Fields { get; private set; } = new List<BadRequestField>();

		public BadRequest AddField(string field, string message)
		{
			this.Fields.Add(new BadRequestField() { Field = field, Message = message });

			return this;
		}

		public class BadRequestField
		{
			public string Field { get; set; }
			public string Message { get; set; }
		}
    }
}
