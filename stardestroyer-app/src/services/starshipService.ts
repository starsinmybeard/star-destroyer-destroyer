import axios from 'axios';
import { Starship } from '../types/Starship';
import { StarshipFilter, PagedResult } from '../types/StarshipFilter';

const API_BASE_URL = 'http://localhost:5069/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const starshipService = {
  getAllStarships: async (): Promise<Starship[]> => {
    const response = await api.get<Starship[]>('/starships');
    return response.data;
  },

  getFilteredStarships: async (filter: StarshipFilter): Promise<PagedResult<Starship>> => {
    const response = await api.post<PagedResult<Starship>>('/starships/filtered', filter);
    return response.data;
  },

  getStarshipClasses: async (): Promise<string[]> => {
    const response = await api.get<string[]>('/starships/starship-classes');
    return response.data;
  },

  getManufacturers: async (): Promise<string[]> => {
    const response = await api.get<string[]>('/starships/manufacturers');
    return response.data;
  },

  getStarshipById: async (id: number): Promise<Starship> => {
    const response = await api.get<Starship>(`/starships/${id}`);
    return response.data;
  },

  createStarship: async (starship: Omit<Starship, 'id'>): Promise<Starship> => {
    const response = await api.post<Starship>('/starships', starship);
    return response.data;
  },

  updateStarship: async (id: number, starship: Starship): Promise<void> => {
    await api.put(`/starships/${id}`, starship);
  },

  deleteStarship: async (id: number): Promise<void> => {
    await api.delete(`/starships/${id}`);
  },

  seedFromSwapi: async (): Promise<string> => {
    const response = await api.post<string>('/starships/seed');
    return response.data;
  },
};
