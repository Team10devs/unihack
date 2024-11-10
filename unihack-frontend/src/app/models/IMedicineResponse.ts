export interface IMedicineResponse{
  name:string,
  dosage : number;
  frequencyPerDay : number;
  startDate ?: string;
  endDate ?:string;
}
