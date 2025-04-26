import React, { useState, useEffect } from 'react';
import { Container, Typography, Box } from '@mui/material';
import { TodoForm } from './components/TodoForm';
import { TodoItem } from './components/TodoItem';
import { Todo } from './types/Todo';
import { todoService } from './services/TodoService';

function App() {
    const [todos, setTodos] = useState<Todo[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const loadTodos = async () => {
        try {
            setLoading(true);
            const data = await todoService.getAll();
            setTodos(data);
            setError(null);
        } catch (error) {
            console.error('Failed to load todos:', error);
            setError('Failed to load todos, please refresh the page and try again');
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadTodos();
    }, []);

    return (
        <Container maxWidth="md">
            <Box sx={{ my: 4 }}>
                <Typography variant="h4" component="h1" gutterBottom align="center">
                    Todo List
                </Typography>
                
                <TodoForm onAdd={loadTodos} />
                
                {loading ? (
                    <Typography align="center">Loading...</Typography>
                ) : error ? (
                    <Typography color="error" align="center">{error}</Typography>
                ) : todos.length === 0 ? (
                    <Typography align="center">No todos yet</Typography>
                ) : (
                    todos.map(todo => (
                        <TodoItem
                            key={todo.id}
                            todo={todo}
                            onUpdate={loadTodos}
                        />
                    ))
                )}
            </Box>
        </Container>
    );
}

export default App;
