// src/api/auth.ts
import { apiClient } from './client';

export const login = async (email: string, password: string) => {
  const response = await apiClient.post('/auth/login', { email, password });
  return response.data; // { token, user }
};