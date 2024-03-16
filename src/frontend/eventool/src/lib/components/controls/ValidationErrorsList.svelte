<script lang="ts">
    type ValidationError = {
        propertyName: string | null,
        errorMessage: string | null
    } | null

    let { errors } = $props<{
        errors: ValidationError[] | undefined
    }>()
</script>

{#if errors}
	<div class="flex flex-col text-red-500 text-sm">
		<ol class="list-decimal">
			{#each errors as error}
				<li>{error?.errorMessage}</li>
			{/each}
		</ol>
	</div>
{/if}
