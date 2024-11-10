public static class BlockTemplateLoader
{
  public static List<Block> LoadBlocksFromTemplate(string[] template, int screenWidth, int screenHeight)
  {
    int templateRows = template.Length;
    int templateCols = template[0].Length;

    // Calculate the maximum block size based on the screen dimensions
    int maxBlockWidth = screenWidth / templateCols;
    int maxBlockHeight = (screenHeight / 3) / templateRows;
    int blockSize = Math.Min(maxBlockWidth, maxBlockHeight);

    // Center the grid horizontally and align vertically from the top
    int startX = (screenWidth - (blockSize * templateCols)) / 2;
    int startY = 0;

    List<Block> blocks = new List<Block>();

    for (int row = 0; row < template.Length; row++)
    {
      string line = template[row];

      for (int col = 0; col < line.Length; col++)
      {
        char blockType = line[col];
        int x = startX + col * blockSize;
        int y = startY + row * blockSize;

        if (blockType == 'x')
        {
          blocks.Add(new Block(x, y, blockSize, blockSize));
        }
      }
    }

    return blocks;
  }
}