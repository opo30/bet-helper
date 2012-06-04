
//
update Schedule  set updated=0 where  updated=1 and id not in (select distinct scheduleid from odds)

delete from schedule  where  updated=1 and id not in (select distinct scheduleid from odds)

select distinct a.id from schedule a where a.id in (select distinct scheduleid from odds where e_win is null ) and year(a.date)=2011 and a.updated=1

select a.id from Schedule a where a.updated=1 and a.id not in (select b.scheduleid from ScheduleRecord b) and year(a.date)=2010

select COUNT(*) from odds

select COUNT(*) from Schedule where updated=1

select COUNT(*) from ScheduleRecord