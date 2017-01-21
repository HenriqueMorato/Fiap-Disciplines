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
            client.BaseAddress = new Uri("http://fiapdisciplines.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

		public Curso GetCurso(int userId)
		{
			return _cursos.Single(c => c.Id == userId.ToString());
		}

		public async Task<List<Curso>> GetAllCurso()
		{
            List<Curso> cursos = new List<Curso>();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //client.DefaultRequestHeaders.Add("Authorization", token);

            HttpResponseMessage response = await client.GetAsync("api/Curso");
            if (response.IsSuccessStatusCode)
            {
                cursos = await response.Content.ReadAsAsync<List<Curso>>();
            }

            _cursos = cursos;

            return _cursos;
		}

		public List<Curso> BuscaCursoPorNome(string filtro = null)
		{
			if (String.IsNullOrWhiteSpace(filtro))
				return _cursos;

			return _cursos.Where(c => c.Titulo.ToLower().Contains(filtro)).ToList();
		}

		public List<Disciplina> DisciplinasPorModulo(Modulo modulo)
		{
			return modulo.Disciplina;
		}

		public void AdicionaCurso (Curso curso)
		{
			_cursos.Add(curso);
		}

		public void RemoveCurso (Curso curso)
		{
			_cursos.Remove(curso);
		}

		public void EditarCurso (string cursoId, Curso curso)
		{
			Curso _curso = _cursos.Single(c => c.Id == cursoId);

			_curso = new Curso
			{
				Id = curso.Id,
				Titulo = curso.Titulo,
				Local = curso.Local,
				Inicio = curso.Inicio,
				Duracao = curso.Duracao,
				Dias = curso.Dias,
				Horario = curso.Horario,
				Investimento = curso.Investimento
			};
		}

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
            string json = JsonConvert.SerializeObject(objeto);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            return stringContent;
        }
    }
}
