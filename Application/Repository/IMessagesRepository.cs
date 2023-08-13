﻿using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Bson;

namespace Application.Repository
{
    public interface IMessagesRepository : IBaseRepository<Message> {}
}
