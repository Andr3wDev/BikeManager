export interface BikeCreateDto {
  brand: string;
  model: string;
  year: number;
  ownerEmail: string;
}

export interface BikeUpdateDto extends BikeCreateDto {
  id: number;
}

export interface BikeDto extends BikeUpdateDto {}