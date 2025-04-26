import React, { useState } from 'react';
import { Card, CardContent, Typography, Button, Box } from '@mui/material';
import { Check, Delete, Undo, Edit } from '@mui/icons-material';
import { Todo } from '../types/Todo';
import { todoService } from '../services/TodoService';
import { EditTodoForm } from './EditTodoForm';

interface TodoItemProps {
    todo: Todo;
    onUpdate: () => void;
}

export const TodoItem: React.FC<TodoItemProps> = ({ todo, onUpdate }) => {
    const [isEditing, setIsEditing] = useState(false);

    const handleToggleComplete = async () => {
        try {
            const updatedTodo = {
                ...todo,
                isCompleted: !todo.isCompleted,
                completedAt: !todo.isCompleted ? new Date().toISOString() : null
            };
            await todoService.update(todo.id, updatedTodo);
            onUpdate();
        } catch (error) {
            console.error('Failed to update todo:', error);
            alert('Failed to update todo, please try again');
        }
    };

    const handleDelete = async () => {
        if (window.confirm('Are you sure you want to delete this todo?')) {
            try {
                await todoService.delete(todo.id);
                onUpdate();
            } catch (error) {
                console.error('Failed to delete todo:', error);
                alert('Failed to delete todo, please try again');
            }
        }
    };

    const handleEdit = async (updatedTodo: Todo) => {
        try {
            await todoService.update(todo.id, updatedTodo);
            setIsEditing(false);
            onUpdate();
        } catch (error) {
            console.error('Failed to update todo:', error);
            alert('Failed to update todo, please try again');
        }
    };

    return (
        <>
            <Card sx={{ mb: 2, bgcolor: todo.isCompleted ? '#f5f5f5' : 'white' }}>
                <CardContent>
                    <Typography variant="h6" component="div" gutterBottom>
                        {todo.title}
                    </Typography>
                    <Typography variant="body2" color="text.secondary" gutterBottom>
                        {todo.description || 'No description'}
                    </Typography>
                    <Typography variant="caption" color="text.secondary">
                        Created at: {new Date(todo.createdAt).toLocaleString()}
                    </Typography>
                    {todo.completedAt && (
                        <Typography variant="caption" color="text.secondary" display="block">
                            Completed at: {new Date(todo.completedAt).toLocaleString()}
                        </Typography>
                    )}
                    <Box sx={{ mt: 2, display: 'flex', gap: 1 }}>
                        <Button
                            variant="contained"
                            color={todo.isCompleted ? 'warning' : 'success'}
                            startIcon={todo.isCompleted ? <Undo /> : <Check />}
                            onClick={handleToggleComplete}
                        >
                            {todo.isCompleted ? 'Mark as Incomplete' : 'Mark as Complete'}
                        </Button>
                        <Button
                            variant="contained"
                            color="primary"
                            startIcon={<Edit />}
                            onClick={() => setIsEditing(true)}
                        >
                            Edit
                        </Button>
                        <Button
                            variant="contained"
                            color="error"
                            startIcon={<Delete />}
                            onClick={handleDelete}
                        >
                            Delete
                        </Button>
                    </Box>
                </CardContent>
            </Card>
            <EditTodoForm
                todo={todo}
                open={isEditing}
                onClose={() => setIsEditing(false)}
                onSave={handleEdit}
            />
        </>
    );
}; 