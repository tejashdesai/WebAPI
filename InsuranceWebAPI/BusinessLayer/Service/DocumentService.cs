using InsuranceWebAPI.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using InsuranceWebAPI.DataLayer;

namespace InsuranceWebAPI.BusinessLayer.Service
{
    public class DocumentService : IDocumentService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Document service constructor
        /// </summary>
        public DocumentService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public int CreateDocument(int policyId, string documentName, string documentPath)
        {
            var document = new Document
            {
                PolicyId = policyId,
                DocumentName = documentName,
                DocumentPath = documentPath
            };
            _unitOfWork.DocumentRepository.Insert(document);
            _unitOfWork.Save();
            return document.DocumentId;
        }

        public bool DeleteDocument(int policyId, string documentName)
        {
            try
            {
                var entity = _unitOfWork.DocumentRepository.GetByCondition(d => d.PolicyId == policyId && d.DocumentName == documentName);
                _unitOfWork.DocumentRepository.Delete(entity);
                _unitOfWork.Save();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}