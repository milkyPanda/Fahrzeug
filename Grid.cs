namespace gridgame
{
    class Grid     //defaults to an internal class
    {
        private Texture2D gridtex;
        
        int width;      //private by default
        int height;
        int pixwidth;
        int pixheight;
        int fieldwidth;
        
        public Grid(GraphicsDevice dev, int w, int h, int fieldwidth)
        {
            this.width = w;
            this.height = h;
            this.pixwidth = width*fieldwidth;
            this.pixheight = h*fieldwidth;
            this.fieldwidth = fieldwidth;
            gridtex = new Texture2D(dev, pixwidth, pixheight );
            
            //Color array for the grid texture
            Color[] col = new Color[pixwidth*pixheight];
            //set whole grid to black
            for(int i = 0; i < pixwidth*pixheight; i++)
            {
                col[i] = Color.Black;
            }
            
            gridtex.setData(col);
        }
        
        public Texture2D get_grid()
        {
            return gridtex;
        }
        
        public int pixwidth()
        {
            return pixwidth;
        }
        
        public int pixheight()
        {
            return pixheight;
        }
        
        public int get_x_pixel_pos(int grid_x_pos)
        {
            return grid_x_pos * fieldwidth;
        }
        
        public int get_y_pixel_pos(int grid_y_pos)
        {
            return grid_y_pos * fieldwidth;
        }
    }
}
