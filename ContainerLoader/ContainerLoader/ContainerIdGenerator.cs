namespace ContainerLoader;

public static class ContainerIdGenerator
{
        private static int _nextId = 1;

        public static int GetNextId()
        {
            return Interlocked.Increment(ref _nextId); 
        }
    }

