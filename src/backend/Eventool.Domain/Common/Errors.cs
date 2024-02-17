using System.Collections;

namespace Eventool.Domain.Common;

public class Errors : IEnumerable<string>
{
    private readonly List<string> _errors = [];

    public bool Happened => _errors.Count != 0;

    public void AddError(string errorMessage) => _errors.Add(errorMessage);


    public IEnumerator<string> GetEnumerator()
        => _errors.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable)_errors).GetEnumerator();
}