using System.Transactions;
using CinemaReserve.ResponseModels;
using CinemaReserve.RestAPI.Models;
using CinemReserve.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Mail;
using CinemaReserve.EntityModels;
using System.Text;

namespace CinemaReserve.RestAPI.Controllers
{
    public class ProjectionsController : BaseApiController
    {
        private const string UserCodeChars = "qwertyuiopasdfghjklzxcvbnm1234567890";
        private const int UserCodeLength = 5;

        public ProjectionDetailsModel GetProjectionDetails(int projectionId)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();
                var theProjection = context.Projections.FirstOrDefault(pr => pr.Id == projectionId);
                if (theProjection == null)
                {
                    throw new ArgumentNullException("projection is either non-existant or is not available");
                }
                return Parser.ToProjectionDetails(theProjection);
            });
        }

        
        [HttpPost]
        public ReservationModel MakeReservation(int projectionId, CreateReservationModel model)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                this.ValidateEmail(model.Email);

                var context = new CinemaReserveDbContext();
                var theProjection = context.Projections.FirstOrDefault(pr => pr.Id == projectionId);
                if (theProjection == null)
                {
                    throw new ArgumentNullException("projection is either non-existant or is not available");
                }

                var reservedSeatStatus = context.SeatStatus.FirstOrDefault(st => st.Status == "reserved") ?? new SeatStatus()
                {
                    Status = "reserved"
                };
                
                List<Seat> reservedSeats = new List<Seat>();

                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (var seat in model.Seats)
                    {
                        var dbSeat = theProjection.Seats.FirstOrDefault(s => s.Row == seat.Row && s.Column == seat.Column);
                        if (dbSeat == null || dbSeat.Status.Status == "reserved")
                        {
                            throw new InvalidOperationException("Seat is not available");
                        }
                        dbSeat.Status = reservedSeatStatus;
                        reservedSeats.Add(dbSeat);
                    }
                    tran.Complete();
                }

                var reservation = new Reservation()
                {
                    ReservedSeats = reservedSeats,
                    UserEmail = model.Email.ToLower(),
                    UserCode = this.GenerateUserCode()
                };


                //context.Reservations.Add(reservation);
                theProjection.Reservations.Add(reservation);
                context.SaveChanges();

                var reservationModel = Parser.ToReservationModel(reservation);

                return reservationModel;
            });
        }

        private string GenerateUserCode()
        {
            StringBuilder userCodeBuilder = new StringBuilder(UserCodeLength);

            while (userCodeBuilder.Length < UserCodeLength)
            {
                var ch = UserCodeChars[rand.Next(UserCodeChars.Length)];
                userCodeBuilder.Append(ch);
            }
            return userCodeBuilder.ToString();
        }

        private void ValidateEmail(string email)
        {
            try
            {
                new MailAddress(email);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException("Invalid email");
            }
        }

        [HttpPut]
        public HttpResponseMessage RejectReservation(int projectionId, RejectReservationModel model)
        {
            return this.ExecuteOperationHandleExceptions(() =>
            {
                var context = new CinemaReserveDbContext();
                var theProjection = context.Projections.FirstOrDefault(pr => pr.Id == projectionId);
                if (theProjection == null)
                {
                    throw new ArgumentNullException("projection is either non-existant or is not available");
                }

                var emailToLower = model.Email.ToLower();
                var userCodeToLower = model.UserCode.ToLower();

                var theReservation = theProjection
                                                  .Reservations
                                                  .FirstOrDefault(r => r.UserCode == userCodeToLower &&
                                                                       r.UserEmail == emailToLower);
                if (theReservation == null)
                {
                    throw new InvalidOperationException("Unauthorized operation");
                }

                var freeSeatStatus = context.SeatStatus.FirstOrDefault(st => st.Status == "free");

                foreach (var seat in theReservation.ReservedSeats)
                {
                    seat.Status = freeSeatStatus;
                    theReservation.ReservedSeats.Remove(seat);
                }
                theProjection.Reservations.Remove(theReservation);
                //context.Reservations.Remove(theReservation);
                context.SaveChanges();
                return this.Request.CreateResponse(HttpStatusCode.NoContent);
            });
        }
    }
}