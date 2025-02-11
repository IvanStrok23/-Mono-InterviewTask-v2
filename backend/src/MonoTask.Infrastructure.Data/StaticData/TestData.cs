using MonoTask.Infrastructure.Data.Entities;

namespace MonoTask.Infrastructure.Data.StaticData;

public class TestData
{
    public List<VehicleBrandEntity> MakeList { get { return getHardCodedMakes(); } private set { } }



    private List<VehicleBrandEntity> getHardCodedMakes()
    {
        List<VehicleBrandEntity> tempList = new List<VehicleBrandEntity>();
        tempList.Add(new VehicleBrandEntity() { Name = "DOK-ING", Country = "Croatia" });
        tempList.Add(new VehicleBrandEntity() { Name = "Alfa Romeo", Country = "Italy" });
        tempList.Add(new VehicleBrandEntity() { Name = "Dacia", Country = "Romania" });
        tempList.Add(new VehicleBrandEntity() { Name = "FAP", Country = "Serbia" });
        tempList.Add(new VehicleBrandEntity() { Name = "Aston Martin", Country = "United Kingdom" });
        tempList.Add(new VehicleBrandEntity() { Name = "Bentley", Country = "United Kingdom" });
        tempList.Add(new VehicleBrandEntity() { Name = "Audi", Country = "Germany" });
        tempList.Add(new VehicleBrandEntity() { Name = "BMW", Country = "Germany" });
        tempList.Add(new VehicleBrandEntity() { Name = "MAN", Country = "Germany" });
        tempList.Add(new VehicleBrandEntity() { Name = "Opel", Country = "Germany" });

        tempList.Add(new VehicleBrandEntity() { Name = "Volkswagen", Country = "Germany" });
        tempList.Add(new VehicleBrandEntity() { Name = "SAZ", Country = "Uzbekistan" });
        tempList.Add(new VehicleBrandEntity() { Name = "Thaco", Country = "Vietnam" });
        return tempList;
    }

    public List<VehicleModelEntity> GetHardCodedModelsByMakeName(string name, int makeId)
    {
        List<VehicleModelEntity> tempList = new List<VehicleModelEntity>();

        switch (name)
        {
            case "DOK-ING":
                tempList.Add(new VehicleModelEntity() { Name = "MV-4", Year = 2010 });
                tempList.Add(new VehicleModelEntity() { Name = "MV-10", Year = 2010 });
                tempList.Add(new VehicleModelEntity() { Name = "MVF-5", Year = 2010 });
                break;
            case "Alfa Romeo":
                tempList.Add(new VehicleModelEntity() { Name = "147", Year = 2010 });
                tempList.Add(new VehicleModelEntity() { Name = "8C Competizione", Year = 2009 });
                tempList.Add(new VehicleModelEntity() { Name = "GT", Year = 2010 });
                tempList.Add(new VehicleModelEntity() { Name = "Spider", Year = 2010 });
                break;
            case "Dacia":
                tempList.Add(new VehicleModelEntity() { Name = "Logan", Year = 2012 });
                tempList.Add(new VehicleModelEntity() { Name = "Logan MCV", Year = 2013 });
                tempList.Add(new VehicleModelEntity() { Name = "Sandero", Year = 2012 });
                break;
            case "FAP":
                tempList.Add(new VehicleModelEntity() { Name = "FAP 1118", Year = 2005 });
                tempList.Add(new VehicleModelEntity() { Name = "FAP 2228", Year = 2006 });
                tempList.Add(new VehicleModelEntity() { Name = "FAP 3240", Year = 2007 });
                break;
            case "Aston Martin":
                tempList.Add(new VehicleModelEntity() { Name = "Vantage", Year = 2010 });
                tempList.Add(new VehicleModelEntity() { Name = "DB11", Year = 2011 });
                tempList.Add(new VehicleModelEntity() { Name = "DBS SUPERLEGGERA", Year = 2012 });
                break;
            case "Bentley":
                tempList.Add(new VehicleModelEntity() { Name = "Mulsanne", Year = 2018 });
                tempList.Add(new VehicleModelEntity() { Name = "Continental", Year = 2019 });
                tempList.Add(new VehicleModelEntity() { Name = "Bentayga", Year = 2020 });
                break;
            case "Audi":
                tempList.Add(new VehicleModelEntity() { Name = "A4", Year = 2001 });
                tempList.Add(new VehicleModelEntity() { Name = "A6", Year = 2003 });
                tempList.Add(new VehicleModelEntity() { Name = "A8", Year = 2008 });
                break;
            case "BMW":
                tempList.Add(new VehicleModelEntity() { Name = "X1", Year = 2008 });
                tempList.Add(new VehicleModelEntity() { Name = "Z4", Year = 2018 });
                tempList.Add(new VehicleModelEntity() { Name = "X7", Year = 2019 });
                break;
            case "MAN":
                tempList.Add(new VehicleModelEntity() { Name = "Lion's Coach", Year = 2012 });
                tempList.Add(new VehicleModelEntity() { Name = "Lion's City 12 E", Year = 2009 });
                tempList.Add(new VehicleModelEntity() { Name = "TGE transporter van", Year = 2005 });
                break;
            case "Opel":
                tempList.Add(new VehicleModelEntity() { Name = "Corsa", Year = 2005 });
                tempList.Add(new VehicleModelEntity() { Name = "Grandland X", Year = 2008 });
                tempList.Add(new VehicleModelEntity() { Name = "MOKKA-E", Year = 2021 });
                break;
            case "Volkswagen":
                tempList.Add(new VehicleModelEntity() { Name = "Tiguan", Year = 2002 });
                tempList.Add(new VehicleModelEntity() { Name = "Passat", Year = 2003 });
                tempList.Add(new VehicleModelEntity() { Name = "Jetta", Year = 2017 });
                break;
            case "SAZ":
                tempList.Add(new VehicleModelEntity() { Name = "LE 60", Year = 2004 });
                break;
            case "Thaco":
                tempList.Add(new VehicleModelEntity() { Name = "DUMP SEMI TRAILER", Year = 2005 });
                tempList.Add(new VehicleModelEntity() { Name = "STAKE CARGO SEMI TRAILER", Year = 2015 });
                break;
            default:
                break;
        }

        return tempList;
    }


}
