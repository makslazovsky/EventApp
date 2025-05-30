﻿namespace Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entityName, object key)
            : base($"Entity \"{entityName}\" ({key}) not found") { }
    }
}
