using System;
using System.Collections.Generic;
using System.Linq;

namespace DisciplinesFiap
{
	public class CursoService
	{
		#region modulos
		private static List<Modulo> _modulos = new List<Modulo>
		{
			new Modulo
			{
				Id = "1",
				Carga = "30",
				Descricao = "Modulo de teste 1",
				Ordem = 1,
				Disciplinas =  new List<Disciplina>
				{
					new Disciplina { Id = "1", Descricao = "teste", Conteudo = "teste"}
				}
			},
			new Modulo
			{
				Id = "2",
				Carga = "30",
				Descricao = "Modulo de teste 2",
				Ordem = 1,
				Disciplinas =  new List<Disciplina>
				{
					new Disciplina { Id = "1", Descricao = "teste", Conteudo = "teste"}
				}
			}
		};
		#endregion
		#region cursos
		private List<Curso> _cursos = new List<Curso>
		{
			new Curso
			{
				Id = "1",
				Titulo = "Curso de Teste 1",
				Local = "FIAP",
				Inicio = DateTime.Now.ToString("ddMMyyyy"),
				Duracao = "30 horas",
				Dias = "ter e qui",
				Horario = "19:00 - 20:00",
				Investimento = "R$ 1000,00",
				Modulos = _modulos
			},
			new Curso
			{
				Id = "2",
				Titulo = "Curso de Teste 2",
				Local = "FIAP",
				Inicio = DateTime.Now.ToString("ddMMyyyy"),
				Duracao = "30 horas",
				Dias = "ter e qui",
				Horario = "19:00 - 20:00",
				Investimento = "R$ 1000,00",
				Modulos = _modulos
			}
		};
		#endregion

		public Curso GetCurso(int userId)
		{
			return _cursos.Single(c => c.Id == userId.ToString());
		}

		public List<Curso> GetAllCurso()
		{
			return _cursos;
		}

		public List<Curso> BuscaCursoPorNome(string filtro = null)
		{
			if (String.IsNullOrWhiteSpace(filtro))
				return _cursos;

			return _cursos.Where(c => c.Titulo.StartsWith(filtro, StringComparison.CurrentCultureIgnoreCase)).ToList();
		}

		public List<Disciplina> DisciplinasPorModulo(Modulo modulo)
		{
			return modulo.Disciplinas;
		}
	}
}
