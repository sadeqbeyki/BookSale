using AppFramework.Domain;

namespace InventoryManagement.Domain.Inventory.Agg
{
    public class InventoryOperation : EntityBase<long>
    {
        public bool Operation { get; private set; }
        public long Count { get; private set; }
        public long OperatorId { get; private set; }
        public DateTime OperationDate { get; private set; }
        public long CurrentCount { get; private set; }
        public string Descriotion { get; set; }
        public long OrderId { get; private set; }
        public long InventoryId { get; private set; }
        public Inventory Inventory { get; private set; }
        protected InventoryOperation() { }

        public InventoryOperation(bool operation, long count, long operationId,
            long currentCount, string descriotion, long orderId, long inventoryId)
        {
            Operation = operation;
            Count = count;
            OperatorId = operationId;
            CurrentCount = currentCount;
            Descriotion = descriotion;
            OrderId = orderId;
            InventoryId = inventoryId;
            OperationDate = DateTime.Now;
        }
    }
}
