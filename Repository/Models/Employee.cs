﻿using TaskManagement.Shared.Repository;

namespace TaskManagement.Repository.Models
{
    public class Employee : EntityBase
    {
        public string Name { get; set; }
        public ICollection<TaskDetail> Tasks { get; set; }
    }
}
