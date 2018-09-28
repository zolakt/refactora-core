using Refactora.Validation.Validator;
using System.Threading.Tasks;

namespace Refactora.Validation.Exporter
{
	public interface IValidationExporter
	{
		Task<object> ExportValidationRules(IValidator validator);
	}
}
