using InsuranceWebAPI.BusinessLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InsuranceWebAPI.Entity;
using InsuranceWebAPI.DataLayer.UnitOfWork;
using InsuranceWebAPI.DataLayer;
using AutoMapper;

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

        public int CreateDocument(int policyId, string documentName)
        {
            var document = new Document
            {
                PolicyId = policyId,
                DocumentName = documentName
            };
            _unitOfWork.DocumentRepository.Insert(document);
            _unitOfWork.Save();
            return document.DocumentId;
        }
    }
}