namespace NorthwindCRUD.Controllers
{
    using AutoMapper;
    using GraphQL.AspNet.Attributes;
    using GraphQL.AspNet.Controllers;
    using NorthwindCRUD.Models.DbModels;
    using NorthwindCRUD.Models.InputModels;
    using NorthwindCRUD.Services;

    [GraphRoute("order")]
    public class OrderGraphController : GraphController
    {
        private readonly OrderService orderService;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public OrderGraphController(OrderService orderService, IMapper mapper, ILogger logger)
        {
            this.orderService = orderService;
            this.mapper = mapper;
            this.logger = logger;
        }

        [Query]
        public OrderInputModel[] GetAll()
        {
            var orders = this.orderService.GetAll();
            return this.mapper.Map<OrderDb[], OrderInputModel[]>(orders);
        }
        
        [Query]
        public OrderInputModel GetById(int id)
        {
            var order = this.orderService.GetById(id);

            if (order != null)
            {
                return this.mapper.Map<OrderDb, OrderInputModel>(order);
            }

            return null;
        }

        [Mutation]
        public OrderInputModel Create(OrderInputModel model)
        {
            var mappedModel = this.mapper.Map<OrderInputModel, OrderDb>(model);
            var order = this.orderService.Create(mappedModel);
            return this.mapper.Map<OrderDb, OrderInputModel>(order);
        }

        [Mutation]
        public OrderInputModel Update(OrderInputModel model)
        {
            var mappedModel = this.mapper.Map<OrderInputModel, OrderDb>(model);
            var order = this.orderService.Update(mappedModel);
            return this.mapper.Map<OrderDb, OrderInputModel>(order);
        }

        [Mutation]
        public OrderInputModel Delete(int id)
        {
            var order = this.orderService.Delete(id);

            if (order != null)
            {
                return this.mapper.Map<OrderDb, OrderInputModel>(order);
            }

            return null;
        }
    }
}
