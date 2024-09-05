import React, { createContext, useState, useContext, useCallback, ReactNode } from 'react';
import { BikeDto, BikeCreateDto, BikeUpdateDto } from '../dto/bikeDto';
import { bikeService } from '../services/bikeService';

interface BikeContextType {
  bikes: BikeDto[];
  fetchBikes: () => void;
  addBike: (bike: BikeCreateDto) => void;
  updateBike: (id: number, bike: BikeUpdateDto) => void;
  deleteBike: (id: number) => void;
  loading: boolean;
  error: string | null;
}

const BikeContext = createContext<BikeContextType | undefined>(undefined);

export const useBikeContext = (): BikeContextType => {
  const context = useContext(BikeContext);
  if (!context) {
    throw new Error('useBikeContext must be used within a BikeProvider');
  }
  return context;
};

export const BikeProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const [bikes, setBikes] = useState<BikeDto[]>([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const fetchBikes = useCallback(async () => {
    setLoading(true);
    try {
      const fetchedBikes = await bikeService.getAllBikes();
      setBikes(fetchedBikes);
      setError(null);
    } catch (err) {
      setError('Failed to fetch bikes');
    } finally {
      setLoading(false);
    }
  }, []);

  const addBike = useCallback(async (bike: BikeCreateDto) => {
    setLoading(true);
    try {
      const newBike = await bikeService.createBike(bike);
      setBikes(prevBikes => [...prevBikes, newBike]);
      setError(null);
    } catch (err) {
      setError('Failed to add bike');
    } finally {
      setLoading(false);
    }
  }, []);

  const updateBike = useCallback(async (id: number, updatedBike: BikeUpdateDto) => {
    setLoading(true);
    try {
      const updated = await bikeService.updateBike(id, updatedBike);
      setBikes(prevBikes => prevBikes.map(b => b.id === id ? updated : b));
      setError(null);
    } catch (err) {
      setError('Failed to update bike');
    } finally {
      setLoading(false);
    }
  }, []);

  const deleteBike = useCallback(async (id: number) => {
    setLoading(true);
    try {
      await bikeService.deleteBike(id);
      setBikes(prevBikes => prevBikes.filter(bike => bike.id !== id));
      setError(null);
    } catch (err) {
      setError('Failed to delete bike');
    } finally {
      setLoading(false);
    }
  }, []);

  return (
    <BikeContext.Provider value={{ bikes, fetchBikes, addBike, updateBike, deleteBike, loading, error }}>
      {children}
    </BikeContext.Provider>
  );
};
