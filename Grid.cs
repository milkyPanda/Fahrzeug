namespace gridgame
{
    class Grid     //defaults to an internal class
    {
        private Texture2D gridtex;
        
        int spacewidth = 5; //defaults to private variable
        int width;
        int height;
        int fieldwidth;
        
        public Grid(GraphicsDevice dev, int w, int h, int fieldwidth)
        {
            this.width = w;
            this.height = h;
            this.fieldwidth = fieldwidth;
            gridtex = new Texture2D(dev, w*fieldwidth + (w+1)*spacewidth, h*fieldwidth + (h+1)*fieldheight );
        }
        
        public Texture2D get_grid()
        {
            return gridtex;
        }
        
        public int get_x_pixel_pos(int grid_x_pos)
        {
            return spacewidth + grid_x_pos * (fieldwidth+spacewidth);
        }
        
        public int get_y_pixel_pos(int grid_y_pos)
        {
            return spacewidth + grid_y_pos * (fieldwidth+spacewidth);
        }
    }
}
