import axios from 'axios';
import { BikeCreateDto, BikeUpdateDto, BikeDto } from '../dto/bikeDto';

const API_URL = process.env.REACT_APP_API_URL + "bike";
if (!API_URL) {
  throw new Error('REACT_APP_API_URL environment variable is not defined');
}

export const bikeService = {
  getAllBikes: async (): Promise<BikeDto[]> => {
    try {
      console.log(API_URL);
      const response = await axios.get(API_URL);
      return response.data.data;
    } catch (error) {
      console.error('Error fetching all bikes:', error);
      throw error;
    }
  },
  getBikeById: async (id: number): Promise<BikeDto> => {
    try {
      const response = await axios.get(`${API_URL}/${id}`);
      return response.data.data;
    } catch (error) {
      console.error(`Error fetching bike with id ${id}:`, error);
      throw error;
    }
  },
  createBike: async (bike: BikeCreateDto): Promise<BikeDto> => {
    try {
      const response = await axios.post(API_URL, bike);
      return response.data.data;
    } catch (error) {
      console.error('Error creating bike:', error);
      throw error;
    }
  },
  updateBike: async (id: number, bike: BikeUpdateDto): Promise<BikeDto> => {
    try {
      const response = await axios.put(`${API_URL}/${id}`, bike);
      return response.data.data;
    } catch (error) {
      console.error(`Error updating bike with id ${id}:`, error);
      throw error;
    }
  },
  deleteBike: async (id: number): Promise<void> => {
    try {
      await axios.delete(`${API_URL}/${id}`);
    } catch (error) {
      console.error(`Error deleting bike with id ${id}:`, error);
      throw error;
    }
  },
};