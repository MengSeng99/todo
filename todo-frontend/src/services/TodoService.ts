import axios from 'axios';
import { Todo } from '../types/Todo';

const API_URL = 'http://localhost:5066/api/todo';

export const todoService = {
    getAll: async (): Promise<Todo[]> => {
        const response = await axios.get<Todo[]>(API_URL);
        return response.data;
    },

    getById: async (id: number): Promise<Todo> => {
        const response = await axios.get<Todo>(`${API_URL}/${id}`);
        return response.data;
    },

    create: async (todo: Omit<Todo, 'id' | 'createdAt' | 'completedAt'>): Promise<Todo> => {
        const response = await axios.post<Todo>(API_URL, todo);
        return response.data;
    },

    update: async (id: number, todo: Partial<Todo>): Promise<void> => {
        await axios.put(`${API_URL}/${id}`, todo);
    },

    delete: async (id: number): Promise<void> => {
        await axios.delete(`${API_URL}/${id}`);
    }
}; 