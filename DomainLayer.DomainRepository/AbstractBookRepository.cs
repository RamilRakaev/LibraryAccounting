using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DomainLayer.DomainRepository;

namespace DomainLayer.DomainModel
{
    public abstract class AbstractBookRepository:AbstractRepository<AbstractBook>
    {
        readonly IEnumerable<AbstractBook> Books;

        public AbstractBook Find(int id)
        {
            foreach (AbstractBook book in Books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            return null;
        }

        public void ChangeTitle(AbstractBook book, string title)
        {
            book.Title = title;
        }

        public void ChangeAuthor(AbstractBook book, string author)
        {
            book.Author = author;
        }

        public void ChangeGenre(AbstractBook book, string genre)
        {
            book.Genre = genre;
        }

        public void ChangePublisher(AbstractBook book, string publisher)
        {
            book.Publisher = publisher;
        }

        abstract public void Save();
    }
}
