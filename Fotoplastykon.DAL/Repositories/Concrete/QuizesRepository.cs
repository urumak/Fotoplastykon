﻿using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.DAL.Repositories.Concrete
{
    public class QuizesRepository : Repository<Quiz>, IQuizesRepository
    {
        public QuizesRepository(DatabaseContext context)
            : base(context)
        {
        }

        private DatabaseContext DatabaseContext => Context as DatabaseContext;

        public Quiz GetFullQuiz(long id)
        {
            return DatabaseContext.Quizes
                .Include(q => q.Questions)
                .ThenInclude(qq => qq.Answers)
                .FirstOrDefault(q => q.Id == id);
        }
    }
}