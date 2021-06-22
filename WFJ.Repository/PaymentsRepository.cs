﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class PaymentsRepository : GenericRepository<Payment>, IPaymentsRepository
    {
        public PaymentsRepository()
        {
        }

        public List<Payment> GetByReqestId(int requestId)
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
            return _context.Payments.Where(x => x.RequestID == requestId).Include(x=> x.User).Include(x => x.Request).Include(x => x.PaymentType).Include(x => x.currency).ToList();
        }
        public List<Payment> GetByClientId(int clientId, DateTime? beginDate, DateTime? endDate,int? ClientUserId)
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;            
            var paymentList= _context.Payments.Where(x => x.User.ClientID == clientId).Include(y => y.Request).Include(x => x.User).Include(x => x.PaymentType).Include(x => x.currency);

            if (ClientUserId != null && ClientUserId!=0)
            {
                IUserClientRepository _UserClientRepo = new UserClientRepository();
                var clientList = _UserClientRepo.GetByUserId(ClientUserId.Value).Select(x => x.Client.ID);
                paymentList = paymentList.Where(x => x.User.ClientID != null && x.User.Client.Active == 1 && clientList.Contains(x.User.Client.ID));
            }

            if (beginDate != null && endDate != null)
            {
                return paymentList.Where(x => x.PaymentDate.Value >= beginDate.Value && x.PaymentDate.Value <= endDate.Value).ToList();
            }
            else if (beginDate != null)
            {
                return paymentList.Where(x => x.PaymentDate.Value >= beginDate.Value).ToList();
            }
            else if (endDate != null)
            {
                return paymentList.Where(x => x.PaymentDate.Value <= endDate.Value).ToList();
            }
            else
            {
                return paymentList.ToList();
            }
        }

        public IEnumerable<Payment> GetPaymentByApprovedAndXDays(int approved)
        {
            return _context.Payments.Where(x => x.approved == approved && x.Request != null).OrderByDescending(x=>x.Request.RequestDate).Take(7);
        }
    }
}
