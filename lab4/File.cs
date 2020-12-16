namespace Backups
{
    public class File
    { 
        public string _fileName { get; }
        public int _size { get; set; }

        public File(string name, int size)
        {
            _fileName = name;
            _size = size;
        }

        public void ChangeSize(int newSize)
        {
            _size = newSize;
        }
    }
}