using DisciplinesFiap.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DisciplinesFiap
{
	public class CursoService
	{
        private const string baseAddress = @"http://fiapdisciplines.azurewebsites.net/";
        private static HttpClient client = new HttpClient();
        private List<Curso> _cursos;
        private string token;
        private static CursoService cursoService;

        public static CursoService getCursoService()
        {
            if (cursoService == null)
            {
                cursoService = new CursoService();
            }

            return cursoService;
        }
        private CursoService()
        {
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #region Curso
        public async Task<Curso> GetCurso(int Id)
		{
            var curso = new Curso();

            HttpResponseMessage response = await client.GetAsync("api/Curso/" + Id.ToString());
            if (response.IsSuccessStatusCode)
            {
                curso = await response.Content.ReadAsAsync<Curso>();
            }

            return curso;
		}

		public async Task<List<Curso>> GetAllCurso()
		{
            List<Curso> cursos = new List<Curso>();

            HttpResponseMessage response = await client.GetAsync("api/Curso");
            if (response.IsSuccessStatusCode)
            {
                cursos = await response.Content.ReadAsAsync<List<Curso>>();
            }

            _cursos = cursos;

            return _cursos;
		}

		public List<Curso> BuscarCursoPorNome(string filtro = null)
		{
			if (string.IsNullOrWhiteSpace(filtro))
				return _cursos;

			return _cursos.Where(c => c.Titulo.ToLower().Contains(filtro)).ToList();
		}

		public async Task<bool> AdicionaCurso(Curso curso)
		{
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PostAsync("api/Curso", ConvertJson(curso));

            return response.IsSuccessStatusCode;
		}

		public async Task<bool> RemoverCurso(Curso curso)
		{
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.DeleteAsync("api/Curso/" + curso.Id.ToString());

            return response.IsSuccessStatusCode;
		}

		public async Task<bool> EditarCurso(Curso curso)
		{
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PutAsync("api/Curso/" + curso.Id.ToString(), ConvertJson(curso));

            return response.IsSuccessStatusCode;
		}
        #endregion

        #region Modulo
        public async Task<bool> AdicionarModulo(Modulo modulo)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PostAsync("api/Modulo", ConvertJson(modulo));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoverModulo(Modulo modulo)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.DeleteAsync("api/Modulo/" + modulo.Id.ToString());

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EditarModulo(Modulo modulo)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PutAsync("api/Modulo/" + modulo.Id.ToString(), ConvertJson(modulo));

            return response.IsSuccessStatusCode;
        }
        #endregion

        #region Disciplina
        public async Task<bool> AdicionarDisciplina(Disciplina disciplina)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PostAsync("api/Disciplina", ConvertJson(disciplina));

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoverDisciplina(Disciplina disciplina)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.DeleteAsync("api/Disciplina/" + disciplina.Id.ToString());

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> EditarDisciplina(Disciplina disciplina)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PutAsync("api/Disciplina/" + disciplina.Id.ToString(), ConvertJson(disciplina));

            return response.IsSuccessStatusCode;
        }
        #endregion

        public async Task<bool> Autenticar(Usuario usuario)
        {
            //Não sei porque cazzo o método de extensão PostAsJsonAsync não rola com o Xamarin
            //Já tinha ocorrido esse erro em outro projeto Xamarin
            //HttpResponseMessage response = await client.PostAsJsonAsync("Autenticar", usuario);
            HttpResponseMessage response = await client.PostAsync("Usuario/Autenticar", ConvertJson(usuario));

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsStringAsync();
                token = token.Replace("\"", "");
                return true;
            }
            else
            {
                return false;
            }
        }

        public static StringContent ConvertJson(object objeto)
        {
            string json = JsonConvert.SerializeObject(objeto,
                                                      Formatting.None,
                                                      new JsonSerializerSettings
                                                      {
                                                          NullValueHandling = NullValueHandling.Ignore
                                                      });
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return stringContent;
        }
    }
}
