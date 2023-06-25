using QuanLyNhaSach.Entities;

namespace QuanLyNhaSach.Processing
{
	public class ParameterProcessing : Processing<Parameter>, IParameter
	{
        public async Task<Parameter> Update(string id, string value, string status)
        {
			if (string.IsNullOrEmpty(id)
				&& string.IsNullOrEmpty(value)
				&& string.IsNullOrEmpty(status)
			)
			{
				throw new Exception("Invalid input");
			}

			Parameter found = await SearchById(id);
            if (found == null)
            {
                throw new Exception("Parameter not found");
            }

			found.UpdatedAt = DateTime.Now;

			if (!string.IsNullOrEmpty(value))
			{
				int _status = int.Parse(status);
				if (_status != 0 && _status != 1)
				{
					throw new Exception("Invalid status");
				}

				found.Status = _status;
			}


			if (!string.IsNullOrEmpty(value)) {
				found.Value = value;
			}

			await _model.UpdateAsync(id, found);
			return found;
        }

        public async Task<Parameter> Search(string name)
		{
			await GetAllAsync();
			if (string.IsNullOrEmpty(name))
			{
				throw new Exception("Invalid input");
			}

			Parameter item = _items.Find(x => x.Name == name);
            return item;
        }
	}
}

