using PaymentOrderWeb.Application.Interfaces;
using PaymentOrderWeb.Domain.Entities;
using PaymentOrderWeb.Domain.Interfaces.Services;
using System.Threading.Tasks.Dataflow;

namespace PaymentOrderWeb.Application.Concrets
{
    public class PaymentOrderAppService : IPaymentOrderAppService
    {
        private readonly IPaymentOrderService _paymentOrderService;

        public PaymentOrderAppService(IPaymentOrderService paymentOrderService)
        {
            _paymentOrderService = paymentOrderService ?? throw new ArgumentNullException(nameof(paymentOrderService));
        }
        public async Task Process(IEnumerable<EmployeeData> employees)
        {
            var fileName = "";
            var groupByName = employees.GroupBy(x => new { x.Name });


            await groupByName.AsyncParallelForEach(async entry =>
            {

                await Validate(entry);
                //validar
                //processar

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());


            ParallelOptions parallelOptions = new()
            {
                MaxDegreeOfParallelism = 100
            };

            await employees.AsyncParallelForEach(async entry =>
            {
                Console.WriteLine($"Processing entry '{entry.Code}'");
                await teste();
                //validar
                //processar

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());






            //_paymentOrderService.Process(employees);
        }

        private async Task Validate(IEnumerable<EmployeeData> employees)
        {
            await employees.AsyncParallelForEach(async entry =>
            {
                Console.WriteLine($"Processing entry '{entry.Code}'");
                await teste();
                //validar
                //processar

            }, 20, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private async Task teste()
        {

        }
    }

    public static class TesteParallel
    {
        //public static async Task AsyncParallelForEach<T>(this IAsyncEnumerable<T> source,
        //                                                                                Func<T, Task> body,
        //                                                                                int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded,
        //                                                                                TaskScheduler scheduler = null)
        //{
        //    var options = new ExecutionDataflowBlockOptions
        //    {
        //        MaxDegreeOfParallelism = maxDegreeOfParallelism
        //    };

        //    if (scheduler != null)
        //        options.TaskScheduler = scheduler;

        //    var block = new ActionBlock<T>(body, options);

        //    await foreach (var item in source)
        //        block.Post(item);

        //    block.Complete();
        //    await block.Completion;
        //}

        public static Task AsyncParallelForEach<T>(this IEnumerable<T> source, Func<T, Task> body, int maxDegreeOfParallelism = DataflowBlockOptions.Unbounded, TaskScheduler scheduler = null)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            if (scheduler != null)
                options.TaskScheduler = scheduler;

            var block = new ActionBlock<T>(body, options);

            foreach (var item in source)
                block.Post(item);

            block.Complete();
            return block.Completion;
        }
    }

}
