﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DisciplinesFiap
{
	public class CursoService
	{
		#region modulos
		private static List<Modulo> _modulos1 = new List<Modulo>
		{
			new Modulo
			{
				Id = "1",
				Carga = "30",
				Descricao = "Modulo de teste 1",
				Ordem = 1,
				Disciplinas =  new List<Disciplina>
				{
					new Disciplina { Id = "1", Descricao = "teste 1", Conteudo = "teste"}
				}
			}
		};

		private static List<Modulo> _modulos2 = new List<Modulo>
		{
			new Modulo
			{
				Id = "2",
				Carga = "30",
				Descricao = "Modulo de teste 2",
				Ordem = 1,
				Disciplinas =  new List<Disciplina>
				{
					new Disciplina { Id = "1", Descricao = "teste 2", Conteudo = "teste"}
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
				Inicio = DateTime.Now.ToString("dd/MM/yyyy"),
				Duracao = "30 horas",
				Dias = "ter e qui",
				Horario = "19:00 - 20:00",
				Investimento = "R$ 1000,00",
				Modulos = _modulos1
			},
			new Curso
			{
				Id = "2",
				Titulo = "Curso de Teste 2",
				Local = "FIAP",
				Inicio = DateTime.Now.ToString("dd/MM/yyyy"),
				Duracao = "30 horas",
				Dias = "ter e qui",
				Horario = "19:00 - 20:00",
				Investimento = "R$ 1000,00",
				Modulos = _modulos2
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
	}
}
