using ApiRest.DTO;
using ApiRest.Models;

namespace ApiRest.Mapper
{
    public class Mappers
    {
        public UserDTO ApplicationUserToUserDTO(ApplicationUser user){
            UserDTO userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.UserName = user.UserName;
            userDTO.AndroidToken = user.AndroidToken;
            userDTO.VerCol = user.VerCol;
            
            return userDTO;
        }

        public OrderDTO OrderToOrderDTO(Order order){
            OrderDTO orderDTO = new OrderDTO();
            orderDTO.OrderNumber = order.OrderNumber;
            orderDTO.State = order.State;
            orderDTO.PickUpDate = order.PickUpDate;
            orderDTO.PickUpStartTime = order.PickUpStartTime;
            orderDTO.PickUpEndTime = order.PickUpEndTime;
            orderDTO.DepositDate = order.DepositDate;
            orderDTO.DepositTime = order.DepositTime;
            orderDTO.DepositStartTime = order.DepositStartTime;
            orderDTO.DepositEndTime = order.DepositEndTime;
            orderDTO.DeliveryType = order.DeliveryType;
            orderDTO.Price = order.Price;
            orderDTO.UserIdOrder = order.UserIdOrder;
            orderDTO.CoursierIdOrder = order.CoursierIdOrder;
            orderDTO.PickUpAddress = order.PickUpAddress;
            orderDTO.DepositAddress = order.DepositAddress;
            orderDTO.BillingAddress = order.BillingAddress;
            orderDTO.VerCol = order.VerCol;
            orderDTO.BillingAddressNavigation = order.BillingAddressNavigation;
            orderDTO.CoursierIdOrderNavigation = ApplicationUserToUserDTO(order.CoursierIdOrderNavigation);
            orderDTO.DepositAddressNavigation = order.DepositAddressNavigation;
            orderDTO.PickUpAddressNavigation = order.PickUpAddressNavigation;
            orderDTO.UserIdOrderNavigation = ApplicationUserToUserDTO(order.UserIdOrderNavigation);
            orderDTO.Letter = order.Letter;
            orderDTO.Parcel = order.Parcel;

            return orderDTO;
        }

        public Order OrderDTOToOrder(OrderDTO orderDTO){
            Order order = new Order();
            order.OrderNumber = orderDTO.OrderNumber;
            order.State = orderDTO.State;
            order.PickUpDate = orderDTO.PickUpDate;
            order.PickUpStartTime = orderDTO.PickUpStartTime;
            order.PickUpEndTime = orderDTO.PickUpEndTime;
            order.DepositDate = orderDTO.DepositDate;
            order.DepositTime = orderDTO.DepositTime;
            order.DepositStartTime = orderDTO.DepositStartTime;
            order.DepositEndTime = orderDTO.DepositEndTime;
            order.DeliveryType = orderDTO.DeliveryType;
            order.Price = orderDTO.Price;
            order.UserIdOrder = orderDTO.UserIdOrder;
            order.CoursierIdOrder = orderDTO.CoursierIdOrder;
            order.PickUpAddress = orderDTO.PickUpAddress;
            order.DepositAddress = orderDTO.DepositAddress;
            order.BillingAddress = orderDTO.BillingAddress;
            order.VerCol = orderDTO.VerCol;
            order.Letter = orderDTO.Letter;
            order.Parcel = orderDTO.Parcel;

            return order;
        }
    }
}