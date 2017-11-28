namespace Bot_B
{
    class FeatureDecoratorGold : ItemDecorator
    {
        //Fields of the class. 
        private Iitem _original_item;

        public FeatureDecoratorGold(Iitem orginalItem)
        {
            _original_item = orginalItem;
        }
        
        
        /**
         * Simple GetDesc method that will return the description of the item and a description of the
         * feature
         */
        public override string GetDesc()
        {
            string seperator = "";
            if (!_original_item.GetDesc().EndsWith(" "))
            {
                seperator = " and ";
            }
            return _original_item.GetDesc() +seperator+ "covered in 5-karat gold"; 
        }
        
        /**
         * Simple GetName method that will return the name of the item name.
         */
        public override string GetName()
        {
            return _original_item.GetName(); 
        }
        
        /**
         * Simple GetPrice method that will return the Item's price + the extra features price. 
         */
        public override double GetPrice()
        {
            return _original_item.GetPrice() + 7.4; 
        }
    }
}