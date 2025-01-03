﻿using ApplicationL.Common.Interfaces;
using ApplicationL.Common.ModelsDto;
using Domain.Entities;

namespace ApplicationL.Queries
{
	public class GetPeopleWithNameDay
	{
		private IReadDataFile _readDataFile;
		private IReadCalendar _readCalendar;
		public GetPeopleWithNameDay(IReadDataFile readDataFile, IReadCalendar readCalendar)
		{
			_readDataFile = readDataFile;
			_readCalendar = readCalendar;
		}

		public IEnumerable<PersonDto> PeopleWithNameDay()
		{
			//Data známych
			IEnumerable<Person> allPeople = _readDataFile.GetAllPeople(); ;

			var calendarDays = new GetCalendar(_readCalendar).CalendarDays();

			Queue<Calendar> calendarQueue = new Queue<Calendar>();

			//Ak v jednom dátume je viacero mien, týmto je dátum pridelený jednotlivo
			foreach (var nameDay in calendarDays)
			{
				if (nameDay.Name.Contains(","))
				{
					string[] separatedNames = nameDay.Name.Split(',');
					foreach (var singleName in separatedNames)
					{
						Calendar calendarDay = new Calendar();
						calendarDay.Name = singleName.Trim();
						calendarDay.Month = nameDay.Month;
						calendarDay.Day = nameDay.Day;
						calendarQueue.Enqueue(calendarDay);
					}
				}
				else
				{
					calendarQueue.Enqueue(nameDay);
				}
			}

			calendarDays = calendarQueue;

			var peopleWithNameDay = from person in allPeople
									join day in calendarDays on person.Name equals day.Name into nameDays
									from nameDay in nameDays.DefaultIfEmpty()
									select new PersonDto
									{
										Id = person.Id,
										Name = person.Name,
										Surname = person.Surname,
										NickName = person.NickName,
										Suffix = person.Suffix,
										DateOfBirth = person.DateOfBirth,
										NameDayMonth = nameDay?.Month ?? null,
										NameDayDay = nameDay?.Day ?? null,
									};

			return peopleWithNameDay;
		}
	}
}
