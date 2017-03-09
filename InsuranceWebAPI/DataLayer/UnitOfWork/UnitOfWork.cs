using InsuranceWebAPI.DataLayer.GenericRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace InsuranceWebAPI.DataLayer.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private InsuranceEntities _context = null;
        private GenericRepository<User> _userRepository;
        private GenericRepository<Policy> _policyRepository;
        private GenericRepository<PolicyHistory> _policyHistoryRepository;
        private GenericRepository<PolicyType> _policyTypeRepository;
        private GenericRepository<Document> _documentsRepository;

        public UnitOfWork()
        {
            _context = new InsuranceEntities();
        }

        /// <summary>
        /// Get/Set Property for policy repository.
        /// </summary>
        public GenericRepository<Policy> PolicyRepository
        {
            get
            {
                if (this._policyRepository == null)
                    this._policyRepository = new GenericRepository<Policy>(_context);
                return _policyRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for policy history repository.
        /// </summary>
        public GenericRepository<PolicyHistory> PolicyHistoryRepository
        {
            get
            {
                if (this._policyHistoryRepository == null)
                    this._policyHistoryRepository = new GenericRepository<PolicyHistory>(_context);
                return _policyHistoryRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for policy type repository.
        /// </summary>
        public GenericRepository<PolicyType> PolicyTypeRepository
        {
            get
            {
                if (this._policyTypeRepository == null)
                    this._policyTypeRepository = new GenericRepository<PolicyType>(_context);
                return _policyTypeRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for user repository.
        /// </summary>
        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this._userRepository == null)
                    this._userRepository = new GenericRepository<User>(_context);
                return _userRepository;
            }
        }

        /// <summary>
        /// Get/Set Property for document repository.
        /// </summary>
        public GenericRepository<Document> DocumentRepository
        {
            get
            {
                if (this._documentsRepository == null)
                    this._documentsRepository = new GenericRepository<Document>(_context);
                return _documentsRepository;
            }
        }

        /// <summary>
        /// Save method.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(string.Format(
                        "{0}: Entity of type \"{1}\" in state \"{2}\" has the following validation errors:", DateTime.Now,
                        eve.Entry.Entity.GetType().Name, eve.Entry.State));
                    foreach (var ve in eve.ValidationErrors)
                    {
                        outputLines.Add(string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage));
                    }
                }
                System.IO.File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw e;
            }

        }

        private bool disposed = false;
        /// <summary>
        /// Protected Virtual Dispose method
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Dispose method
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}